# Foundation
A theme for [StatiC#](https://www.nuget.org/packages/StatiCSharp/), a static webside generator written in C#.  
Supports light and dark mode.

## Installation

Add **Foundation** to your StatiC# project as a package reference in the .csproj file:

```
<ItemGroup>
    <PackageReference Include="StatiCSharp.Theme.Foundation" Version="0.2.1" />
</ItemGroup>
``` 
Build your project to restore packages.  
You can then import Foundation at the top of your `Program.cs` and inject the theme to StatiC#'s website generating process by initializing a new FoundationHtmlFactory:

```C#
using StatiCSharp;
using Foundation;

var myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: "posts, about"
);

var theme = new FoundationHtmlFactory(website: myAwesomeWebsite);
// Set up social icon here if needed.

var manager = new WebsiteManager(
    website: myAwesomeWebsite,
    htmlFactory: theme,                 // Here Foundation is injected to the generating process.
    source: @"/path/to/your/project"
);

await manager.Make();
```

To set the portrait image on the index page, provide a `me.jpg` image in the root of your `Resources` directory.

## Advanced settings

You can configure Foundation after initializing FoundationHtmlFactory to show social icons on the top right corner of your website. Set the property to the target page of your social networks.

```C#
theme.Email = "mailto:mail@yourdomain.com";
theme.Facebook = "https://facebook.com/yourName";
```

Foundation currently supports social icons for E-Mail, LinkedIn, GitHub, Facebook, Twitter, Instagram, YouTube and Teams.  

To set legal notice and/or privacy links in the footer use:

```C#
theme.LegalNotice = "/your/logalNoticePage";
theme.Privacy = "/your/privacyPage";
```
