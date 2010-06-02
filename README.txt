A plugin for ReSharper 5.1 which makes it simple to change a method declaration from

    public void "Test that something is foobar"()

to

    public void Test_that_something_is_foobar()

This mimics the behavior of the Visual Studio macro posted by David Starr at http://elegantcode.com/2010/05/17/boxcar-case-macro-in-visual-studio-2010-video/

The plugin is implemented as a ContextAction plugin. When you have the caret on a line with a string literal (ex: "This is a test") and the line ends with a ) character, then Alt-Enter will provide an action for 'Convert from spaces to underscore in string literals in method declarations'

Installation:
1. Download the plugin from the downloads page (assume that this requires the same build of R# installed) and put it in your ReSharper plugin folder (ex: C:\Program Files\JetBrains\ReSharper\v5.1\Bin\Plugins)

2. Build it from source:
    - Open the solution in VS2010 and fix the paths to the references. They should point to assemblies located in your ReSharper bin folder.
    - Build the solution and copy BoxcarMethodNamePlugin.dll to your ReSharper plugin folder (ex: C:\Program Files\JetBrains\ReSharper\v5.1\Bin\Plugins)
    - Restart Visual Studio