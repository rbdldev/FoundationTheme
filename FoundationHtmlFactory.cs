using StatiCSharp.HtmlComponents;
using StatiCSharp.Interfaces;

namespace Foundation;

public class FoundationHtmlFactory : IHtmlFactory
{
    public string Email { get; set; } = string.Empty;
    public string LinkedIn { get; set; } = string.Empty;
    public string Github { get; set; } = string.Empty;
    public string Facebook { get; set; } = string.Empty;
    public string Twitter { get; set; } = string.Empty;
    public string Instagram { get; set; } = string.Empty;
    public string Youtube { get; set; } = string.Empty;
    public string Teams { get; set; } = string.Empty;
    public string LegalNotice { get; set; } = string.Empty;
    public string Privacy { get; set; } = string.Empty;
    public string Copyright { get; set; } = string.Empty;
    private Dictionary<string, string> SocialIconsMap
    {
        get
        {
            return new Dictionary<string, string>()
            {
                { "Email", Email },
                { "LinkedIn", LinkedIn },
                { "GitHub", Github },
                { "Facebook", Facebook },
                { "Twitter", Twitter },
                { "Instagram", Instagram },
                { "YouTube", Youtube },
                { "Teams", Teams }
            };
        }
    }

    public string ResourcesPath
    {
        get
        {
            string? path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(path!, "FoundationResources");
        }
    }

    private IWebsite Website { get; set; }

    public FoundationHtmlFactory(IWebsite website)
    {
        Website     = website;
    }

    public string MakeHeadHtml()
    {
        return "<link rel =\"stylesheet\" href=\"/foundation-theme/styles.css\">";
    }

    public string MakeIndexHtml(IIndex index)
    {
        // Collect all items to show. 10 items max.
        List<IItem> items = new List<IItem>();
        foreach (ISection section in Website.Sections)
        {
            section.Items.ForEach((item) => items.Add(item));
        }
        int showArticles = (items.Count > 10) ? 10 : items.Count;
        // http://procbits.com/2010/09/09/three-ways-to-sort-a-list-of-objects-with-datetime-in-c
        items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
        items.Reverse();
        items = items.GetRange(0, showArticles);

        return new Body().Add(new SiteHeader(website: Website, socialIconsMap: SocialIconsMap))
                            .Add(new Div()
                                .Add(new Div()
                                        .Add(new Image("/me.jpg")
                                                .Class("portraitImage"))
                                        .Add(new Text(index.Content))
                                        .Class("welcomeWrapper"))
                                .Add(new H2("Latest Content"))
                                .Add(new ItemList(items))
                                .Class("wrapper"))
                            .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy, copyright: Copyright))
                .Render();
    }

    public string MakePageHtml(IPage page)
    {
        return new Body().Add(new SiteHeader(website: Website, socialIconsMap: SocialIconsMap))
                            .Add(new Div()
                                .Add(new Article()
                                    .Add(new Div(page.Content)
                                        .Class("content")))
                                .Class("wrapper"))
                            .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy, copyright: Copyright))
                .Render();
    }

    public string MakeSectionHtml(ISection section)
    {
        List<IItem> items = section.Items;
        items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
        items.Reverse();
        return new Body().Add(new SiteHeader(website: Website, socialIconsMap: SocialIconsMap))
                            .Add(new Div(section.Content)
                                .Class("wrapper"))
                            .Add(new Div()
                                .Add(new ItemList(items))
                                .Class("wrapper"))
                            .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy, copyright: Copyright))
                .Render();
    }

    public string MakeItemHtml(IItem item)
    {
        return new Body().Add(new SiteHeader(website: Website, socialIconsMap: SocialIconsMap))
                            .Add(new Div()
                                .Add(new TagList(item.Tags))
                                .Add(new Text(item.Date.ToString("MMMM dd, yyyy")))
                                .Class("item-meta-data-header"))
                            .Add(new Div()
                                .Add(new Article()
                                    .Add(new Div(item.Content)
                                        .Class("content")))
                                .Class("wrapper"))
                            .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy, copyright: Copyright))
                .Render();
    }

    public string MakeTagListHtml(List<IItem> items, string tag)
    {
        items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
        items.Reverse();
        return new Body().Add(new SiteHeader(website: Website, socialIconsMap: SocialIconsMap))
                            .Add(new Div()
                                .Add(new H1()
                                    .Add(new Text("Tagged with "))
                                    .Add(new BigTag(tag)))
                                .Add(new ItemList(items))
                                .Class("wrapper"))
                            .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy, copyright: Copyright))
                .Render();
    }


    ////////////
    /// Custom Components
    ////////////

    private class SiteHeader : IHtmlComponent
    {
        private Dictionary<string, string> _socialIconsMap;

        private List<string> _sections;
        private IWebsite _website;

        public SiteHeader(IWebsite website, Dictionary<string, string> socialIconsMap)
        {
            _website = website;
            _socialIconsMap = socialIconsMap;
            _sections = website.MakeSectionsFor;
        }

        public string Render()
        {
            Ul NavLinks = new();
            foreach (var section in _sections)
            {
                if (section.ToString() is not null)
                {
                    // Make section name first char uppercase.
                    string uppercaseSection = char.ToUpper(section[0]) + section.Substring(1);
                    NavLinks.Add(new Li(new A(uppercaseSection).Href($"/{section}")));
                }
            }
            return new Header(
                            new Div(
                                new A(_website.Name).Href("/").Class("site-name")
                            ).Add(
                                new Nav().Add(
                                    new Ul().Add(NavLinks)
                                )
                            ).Class("wrapper")
                    )
                    .Add(new SocialIcons(socialIconsMap: _socialIconsMap))
                    .Render();
        }
    }

    private class SocialIcons : IHtmlComponent
    {
        private Dictionary<string, string> _socialIconsMap;

        public SocialIcons(Dictionary<string, string> socialIconsMap)
        {
            _socialIconsMap = socialIconsMap;
        }

        public string Render()
        {
            var div = new Div();

            foreach (KeyValuePair<string, string> social in _socialIconsMap)
            {
                if (!string.IsNullOrEmpty(social.Value))
                {
                    div.Add(new A($"<img src=\"/foundation-theme/socialIcons/{social.Key.ToLower()}.svg\">").Href($"{social.Value}").Target("_blank"));
                }
            }

            div.Class("social-icons");
            return div.Render();
        }
    }

    private class ItemList : IHtmlComponent
    {
        private List<IItem> _items;
        public ItemList(List<IItem> items)
        {
            _items = items;
        }
        public string Render()
        {
            var result = new Ul().Class("item-list");
            _items.ForEach((item) => result.Add(
                                            new Li()
                                                .Add(new Article()
                                                    .Add(new H1().Add(
                                                                new A(item.Title).Href(item.Url)
                                                            )
                                                    )
                                                    .Add(new Div()
                                                            .Add(new TagList(item.Tags))
                                                            .Add(new Text(item.Date.ToString("MMMM dd, yyyy")))
                                                            .Class("item-meta-data"))
                                                    .Add(new Text($"<p>{item.Description}</p>"))
                                                )
                                            )
                                    );
            return result.Render();
        }
    }

    private class TagList : IHtmlComponent
    {
        private List<string> _tags;
        public TagList(List<string> tags)
        {
            _tags = tags;
        }
        public string Render()
        {
            var result = new Ul().Class("tags");
            _tags.ForEach((tag) => result.Add(
                                            new Li().Class("variant-default")
                                                    .Add(new A(tag).Href($"/tag/{tag}")))
                        );
            return result.Render();
        }
    }

    private class BigTag : IHtmlComponent
    {
        private string _tag;
        public BigTag(string tag)
        {
            _tag = tag;
        }
        public string Render()
        {
            var result = new Span(_tag).Class("tag");
            return result.Render();
        }
    }

    private class Footer : IHtmlComponent
    {
        private readonly string _legal;
        private readonly string _privacy;
        private readonly string _copyright;

        public Footer(string legalNotice="", string privacy="", string copyright="")
        {
            _legal = legalNotice;
            _privacy = privacy;
            _copyright = copyright;
        }

        public string Render()
        {
            var footer = new StatiCSharp.HtmlComponents.Footer();
            footer.Add(new Paragraph()
                    .Add(new Text("Generated with ❤️ using "))
                    .Add(new A("StatiC#").Href("https://github.com/RolandBraunDev/StatiCSharp").Target("_blank")));

            if (!string.IsNullOrEmpty(_legal) | !string.IsNullOrEmpty(_privacy))
            {
                var legal = new Paragraph();
                
                if (!string.IsNullOrEmpty(_privacy))
                {
                    legal.Add(new A("Privacy").Href(_privacy));
                }

                if (!string.IsNullOrEmpty(_legal))
                {
                    legal.Add(new A("Legal notice").Href(_legal).Style("padding-left: 20px"));
                }

                footer.Add(legal);
            }

            if (!string.IsNullOrEmpty(_copyright))
            {
                var copyright = new Paragraph($"© {_copyright}");
                footer.Add(copyright);
            }

            return footer.Render();
        }
    }
}