# JOS.Epi.ContentApi
Headless Episerver API without find and with pretty urls.

## How to use it

1. ```Install-Package Jos.Epi.ContentApi```(Normal nuget, not episerver feed)
2. Set your accept header to "application/json" and make a GET request to your desired page. **Note:** When the package gets installed, a transform to your web.config will be applied and add the following line:
```<add name="ContentApiModule" type="JOS.Epi.ContentApi.ContentApiModule, JOS.Epi.ContentApi" xdt:Transform="Insert" />```
If it doesn't work, add it yourself at ```/configuration/system.webServer/modules/```
3. Profit.
