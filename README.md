# JOS.Epi.ContentApi
Headless Episerver API without find and with pretty urls.

## DEMO
https://www.youtube.com/watch?v=cnqB4EQgSy0

## How to use it

1. ```Install-Package JOS.Epi.ContentApi```(Normal nuget, not episerver feed)
2. Set your accept header to "application/json" and make a GET request to your desired page.
3. Profit.

## How does it work?
1. Gets the current ```IShouldSerializeResponseStrategy``` and executes it.
2. If ```IShouldSerializeResponseStrategy``` returns false, the module returns.
3. If ```IShouldSerializeResponseStrategy``` returns true we get the current ```IUrlResolver``` and tries to route the current path and map it to ```IContent```.
4. If no content is found the module returns.
5. If content is found -> we get the current ```IContentApiSerializer``` and calls ```.Serialize(contentData)``` and then returns the result.

## Flexibility

You can swap out the following interfaces

* ```IShouldSerializeResponseStrategy``` - Responsible for deciding if the response should be serialized. Uses [DefaultShouldSerializeResponseStrategy](https://github.com/joseftw/JOS.Epi.ContentApi/blob/develop/src/JOS.Epi.ContentApi/Internal/DefaultShouldSerializeResponseStrategy.cs) by default
* ```ISupportedAcceptTypesStrategy``` - Decides which accept types you support, default is ```application/json```. Uses [DefaultSupportedAcceptTypesStrategy](https://github.com/joseftw/JOS.Epi.ContentApi/blob/develop/src/JOS.Epi.ContentApi/Internal/DefaultSupportedAcceptTypesStrategy.cs) by default.
* ```IContentApiSerializer``` - Decides which serializer to use. Uses [DefaultContentApiSerializer](https://github.com/joseftw/JOS.Epi.ContentApi/blob/develop/src/JOS.Epi.ContentApi/Internal/DefaultContentApiSerializer.cs) by default which itself uses [JOS.ContentSerializer](https://github.com/joseftw/JOS.ContentSerializer).
