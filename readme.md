# MVC Flash Messages

This project adds Rails-style "flash" messages to your MVC projects. The flash messages are stored in MVC's [TempData](http://msdn.microsoft.com/en-us/library/system.web.mvc.tempdatadictionary.aspx). They will persist through a redirect and are removed when the collection is enumerated.

## Building and Dependencies

This project is built with the following tools.

- Visual Studio 2013
- MVC 3.0
- NUnit 2.6
- Code Contracts
- psake

To build the project, run the tests, or create a NuGet package, use [psake](https://github.com/psake/psake). The following tasks have been defined in the project.

- **Version** &mdash; Update version. Invoke with `Invoke-psake Version @{ "version" = "a.b.c" }`. This will update the version in both the `AssemblyInfo.cs` and the `MvcFlashMessages.nuspec` files.
- **NUnit** &mdash; Compile a debug build and run the [NUnit](http://nunit.org) tests.
- **CreatePackage** &mdash; Create a new NuGet package. The `MvcFlashMessages.*.nupkg` will be placed in the `nuget` folder.

This project also makes use of [Code Contracts](http://visualstudiogallery.msdn.microsoft.com/1ec7db13-3363-46c9-851f-1ce455f66970). If you wish to compile the project, you will need to have the Visual Studio plugin.

## Usage

To use flash messages in your project, first add the NuGet package. From your controller, you can add a flash message with the following key and value.

    public ActionResult MyAction()
	{ 
        service.Invoke(); // Do something awesome.
        this.Flash("info", "Hey! The service was invoked. Isn't that awesome?");
        return View();
    }

To display flash messages, add the following to your view. *Tip: add to your layout for consistency.*

    @Html.RenderFlash();

This HTML helper method would add the following to your view.

    <div class="flash-messages">
      <div class="flash-message flash-message-info">
        Hey! The service was invoked. Isn't that awesome?
      </div>
    </div>

The flash helper will loop through all of your flash messages and display each of them in their own `<div>` tag.

    <div class="flash-messages">
      <div class="flash-message flash-message-success">
        This is my first message.
      </div>
      <div class="flash-message flash-message-info">
        This is my second message.
      </div>
    </div>

### Default message types

This NuGet package will drop a CSS file in your project located at `~/Content/flash-messages.css`. The following styles come predefined. *Feel free to add your own!*

- **Success** &mdash; Something good. Log on succeeded, database was updated, etc.
- **Info** &mdash; Informational messages.
- **Warning** &mdash; Something not so great.
- **Error** &mdash; Something terrible.

## Settings

A few values can be set in your `web.config` file.

| Setting | Default Value | Description |
|---------|---------------|-------------|
| MvcFlashMessages/InnerCssClass | flash-message | Inner container CSS class. |
| MvcFlashMessages/OuterCssClass | flash-messages | Outer container CSS class. |

## Release Notes

- 0.2.2 - Added code contracts.
- 0.2.1 - Bugfix release. The `FlashMessage` class needs to be marked as serializable to work with State Server or SQL-backed sessions.
- 0.2.0 - Added extension method to get flash messages out of TempData. This makes testing scenarios a bit easier. Also, the temp data key is now exposed as the static property `FlashMessageCollection.Key`.
- 0.1.1 - Updated NuGet metadata.
- 0.1.0 - First release.
