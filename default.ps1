properties {

    $sourcePath = ".\source"
    $projectPath = Join-Path $sourcePath MvcFlashMessages
    $projectBinPath = Join-Path $projectPath bin
    $projectObjPath = Join-Path $projectPath obj
    $projectFile = Join-Path $projectPath MvcFlashMessages.csproj
    $nunitFramework = "net-4.0"
    $assemblyInfoPath = Join-Path $projectPath Properties/AssemblyInfo.cs
    $nugetSpecPath = ".\nuget\MvcFlashMessages.nuspec"

}

task default -depends CompileDebug

task Clean {
    # Delete all existing bin\ and obj\ directories.
    try {
        Remove-Item -Recurse -Force $projectBinPath
        Remove-Item -Recurse -Force $projectObjPath
    }
    catch {
        Write-Host Nothing to clean!
    }
}

task CompileDebug -depends Clean, Version {        
    msbuild.exe /p:Configuration=Debug $projectFile
}

task CompileRelease -depends NUnit {
    msbuild.exe /p:Configuration=Release $projectFile
}


task CreatePackage -depends CompileRelease {
    nuget.exe pack $nugetSpecPath
    Move-Item .\*.nupkg .\nuget
}

task NUnit -depends CompileDebug {    
    #Compile the unit test project in debug mode.
    msbuild.exe /p:Configuration=Debug .\source\MvcFlashMessages.Tests\MvcFlashMessages.Tests.csproj
    nunit-console.exe /framework=$nunitFramework .\source\MvcFlashMessages.Tests\bin\Debug\MvcFlashMessages.Tests.dll
}

task Version {
    if ($version -eq $null) {
        Write-Host No version update specified.
        Write-Host To update the version, include '-parameters @{ "version" = "1.0.0" }' when you invoke psake
        return
    }
    Write-Host Updating project to version $version ...
    Update-AssemblyInfo $assemblyInfoPath $version
    Update-NugetSpec $nugetSpecPath $version
}

function Update-AssemblyInfo($file, $version) {
    $content = Get-Content $file
    $assemblyVersionPattern = 'AssemblyVersion\("\d+\.\d+\.\d+\.\d+"\)'    
    $updatedAssemblyVersion = 'AssemblyVersion("' + $version + '.0")'
    $assemblyFileVersionPattern = 'AssemblyFileVersion\("\d+\.\d+\.\d+\.\d+"\)'
    $updatedAssemblyFileVersion = 'AssemblyFileVersion("' + $version + '.0")'
    $content = $content -replace $assemblyVersionPattern, $updatedAssemblyVersion
    $content = $content -replace $assemblyFileVersionPattern, $updatedAssemblyFileVersion
    Set-Content $file $content
}

function Update-NugetSpec($file, $version) {
    $content = Get-Content $file
    $versionPattern = '<version>\d+\.\d+\.\d+</version>'
    $versionReplacement = '<version>' + $version + '</version>'
    $content = $content -replace $versionPattern, $versionReplacement
    Set-Content $file $content
}