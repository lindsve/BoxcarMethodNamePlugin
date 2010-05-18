A plugin for ReSharper 5.0 which makes it simple to change a method declaration from

    public void "Test that something is foobar"()

to

    public void Test_that_something_is_foobar()

This mimics the behavior of the Visual Studio macro posted by David Starr at http://elegantcode.com/2010/05/17/boxcar-case-macro-in-visual-studio-2010-video/

The plugin is implemented as a ContextAction plugin. When you have the caret on a line with a public method where the name is a string literal, then Alt-Enter will provide an action for 'Convert from spaces to underscore in string literals'

The plugin is currently just hacked together quite quickly and I expect that it can be improved and optimized further.

Installation:
- Open the solution in VS2010 and fix the paths to the references. They should point to assemblies located in your ReSharper bin folder.
- Build the solution and copy BoxcarMethodNamePlugin.dll to your ReSharper plugin folder (example: C:\Program Files\JetBrains\ReSharper\v5.0\Bin\Plugins)
- Restart Visual Studio