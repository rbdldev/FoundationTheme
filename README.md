<p align="center">
    <img src="Images/Logo.png" width="400" max-width="90%" alt="StatiC#" />
</p>

<p align="center">
    <a href="https://docs.microsoft.com/en-us/dotnet/csharp/">
        <img src="https://img.shields.io/badge/C%23-10.0-blue?style=flat" alt="C# 10.0" />
    </a>
    <a href="https://dotnet.microsoft.com">
        <img src="https://img.shields.io/badge/.NET-6.0-blueviolet?style=flat" />
    </a>
    <a href="https://github.com/RolandBraunDev/StatiCsharp">
        <img src="https://img.shields.io/static/v1?label=StatiC%23&message=0.1&color=informational" />
    </a>
    <img src="https://img.shields.io/badge/Platforms-Win+Mac+Linux-green?style=flat" />
    <img src="https://img.shields.io/badge/Version-0.1.0--alpha4-green?style=flat" />
    <a href="https://www.nuget.org/packages/StatiCsharp.Theme.Foundation">
        <img src="https://img.shields.io/nuget/v/StatiCsharp.Theme.Foundation?color=orange" />
    </a>
</p>

A theme for [StatiC#](https://github.com/RolandBraunDev/StatiCsharp), a static website generator written in C#.

## Example

<p align="center">
    <img src="Images/landing_page_example.png" max-width="90%" alt="StatiC#" />
</p>

## Installation

Add **Foundation** to your StatiC# project as a package reference in the .csproj file:

```
<ItemGroup>
    <PackageReference Include="StatiCsharp.Theme.Foundation" Version="0.1.0-alpha4" />
</ItemGroup>
``` 
Build your project to restore packages.  
You can then import Foundation at the top of your `Program.cs` and inject the theme to StatiC#'s website generating process by initializing a new FoundationHtmlFactory:

```C#
using StatiCsharp;
using Foundation;

var myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: "posts, about"
);

var theme = new FoundationHtmlFactory();
// Set up social icon here if needed.

var manager = new WebsiteManager(
    website: myAwesomeWebsite,
    htmlFactory: theme,                 // here Foundation is injected to the generating process
    source: @"/path/to/your/project"
);

manager.Make();
```

To set the portrait image on the index page, provide a `me.jpg` image in the root of your `Resources` directory.

## Advanced settings

You can configure Foundation after initializing FoundationHtmlFactory to show social icons on the top right corner of your website. Set the property to the target page of your social networks.

```C#
theme.Email = "mail@yourdomain.com";
theme.Facebook = "https://facebook.com/yourName";
```

Foundation currently supports social icons for E-Mail, LinkedIn, GitHub, Facebook, Instagram and YouTube.

 <img src="Images/social_icons_example.png" width="200" max-width="70%" alt="Example for social icons" />

To set legal notice and/or privacy links in the footer use:

```C#
theme.LegalNotice = "/your/logalNoticePage";
theme.Privacy = "/your/privacyPage";
```

<img src="Images/legal_example.png" width="300" max-width="70%" alt="Example for legal notice" />
