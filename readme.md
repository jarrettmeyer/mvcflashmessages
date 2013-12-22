# MVC Flash Messages

This project adds Rails-style "flash" messages to your MVC projects. The flash messages are stored in MVC's [TempData](http://msdn.microsoft.com/en-us/library/system.web.mvc.tempdatadictionary.aspx). They will persist through a redirect and are removed when the collection is enumerated.

## Building and Dependencies

This project is built with the following tools.

- Visual Studio 2013
- MVC 3.0
- NUnit 2.6

## Usage

To use flash messages in your project, first add the NuGet package. From your controller, you can add a success message.

    public ActionResult MyAction()
	{ 
        service.Invoke(); // Do something awesome.
        this.Flash("info", "Hey! The service was invoked. Isn't that awesome?");
        return View();
    }

To display flash messages, add the following to your view.

    @Html.Flash();

This HTML helper method would add the following to your view.

    <div class="flash-messages">
      <div class="flash-message flash-message-info">
        "Hey! The service was invoked. Isn't that awesome?"
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

## Tests

Notice any missing tests? Let me know or submit a pull request.