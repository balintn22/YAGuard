
# YAGuard

Yet Another Guard argument validation package.
Provide the argument for validation, its name is automatically resolved in the resulting exception.
Usage:
```
public void MyFunc(string myArg)
{
    Guard.AgainstNull(myArg);
    // or
    Guard.AgainstNull( () => myArg );
    // or
    Guard.AgainstNull(myArg, "myArg");
    // or
    Guard.AgainstNull(myArg, nameof(myArg));
}
```
In case myArg is null, this will throw an ArgumentNullException with the correct argument name.

You can also use value of the argument immediately like this:
```
public void MyFunc(string myArg)
{
    string validatedArg1 = Guard.AgainstNull(myArg);
    // or
    string validatedArg2 = Guard.AgainstNull( () => myArg );
    // or
    string validatedArg3 = Guard.AgainstNull(myArg, "myArg");
    // or
    string validatedArg4 = Guard.AgainstNull(myArg, nameof(myArg));
}
```
## Examples
To validate method arguments:
```
  public void MyMethod(string myArg)
  {
    Guard.AgainstNull(myArg);              // Will throw ArgumentNullException when myArg is null.
    Guard.AgainstNull(() => myArg);        // Will throw ArgumentNullException when myArg is null.
    Guard.AgainstNullOrEmptyString(myArg); // Will throw ArgumentException when myArg is null or empty.
    Guard.AgainstLongString(myArg, 10);    // Will throw ArgumentException when myArg is longer then 10 characters.
    
    MyProperty = myArg;
    ...
  }
```
Use intellisense for a full list of supported validation methods.

## Limitations
All styles detect invalid parameter values correctly, but there are differences in the efficiency in resolving parameter names.
The expression style : `Guard.AgainstNull(() => myArg);` reliably resolves the name of the parameter in most scenarios.

The simple style: `Guard.AgainstNull(myArg)` works correctly when there's a single method parameter, or when the type of the guarded parameter is unique in the argument list.

In case you are validating anything other than a method parameter, use the expression style, as the simple style uses the method declaration to resolve the parameter name.
In case there are two parameters of the same type, the parameter name in the exception thrown will contain both of their names.
In such cases, use the expression style.


# Release History
## v1.1.4
Added support for validating collection argument items against a set of accepted values.
    `Guard.AgainstUnsupportedCollectionItems(fruits, new string[] { "apples", "pears"});`

## v1.1.x
Added support for the expression style: `Guard.Against...( () => myArg );`
Added best effort parameter name resolution to the plain style validation.

## v1.0.4
Retired the Assign class, changed Guard methods to return the argument value.

## v1.0.x
Added support for the style:  `Guard.Against...(myArg);` which automatically resolves the name of the argument.

Removed support for the style: `Guard.Against...(new { myArg });`

## v0.0.x
Implemented two styles:
```
Guard.Against...(myArg, argName);
Guard.Against...(new { myArg });
```
the second one to help avoid typing the name of the argument. But it didn't really help when compared to nameof(myArg).
