# YAGuard

Yet Another Guard argument validation package.
Provide the argument for validation, its name is automatically resolved in the resulting exception.
Usage:
public void MyFunc(string myArg)
{
    Guard.AgainstNull(myArg);
}
In case myArg is null, this will throw an ArgumentNullException with the correct argument name.

Supports single line argument checking and assignment as well:
string result = Assign.NotNull(string source);
will set result to source in case it is not null, or throw an appropriate ArgumentNullException in case it is null.

##Examples
To validate method arguments:
  public void MyMethod(string myArg)
  {
    Guard.AgainstNull(myArg);              // Will throw ArgumentNullException when myArg is null.
    Guard.AgainstNullOrEmptyString(myArg); // Will throw ArgumentException when myArg is null or empty.
    Guard.AgainstLongString(myArg, 10);    // Will throw ArgumentException when myArg is longer then 10 characters.
    
    MyProperty = myArg;
    ...
  }

Use intellisense for a full list of supported validation methods.

To use a single line of code to validate and assign values to variables:
  public void MyMethod(string myArg)
  {
    MyProperty = Assign.NotNull(myArg);     // Will throw ArgumentNullException when myArg is null, or assign myArg to MyProperty if not null.
    ...
  }
  

#Release History
##v1.0.x
Added support for the style
  Guard.Against...(myArg);
which automatically resolves the name of the argument.

Removed support for the style
  Guard.Against...(new { myArg });

##v0.0.x
Implemented two styles:
   Guard.Against...(myArg, argName);
   Guard.Against...(new { myArg });
the second one to help avoid typing the name of the argument. But it didn't really help when compared to nameof(myArg). 