using StatiCsharp.HtmlComponents;
using StatiCsharp.Interfaces;

namespace Foundation
{
    public class FoundationHtmlFactory : IHtmlFactory
    {
        public string Email { get; set; }
        public string Linkedin { get; set; }
        public string Github { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string LegalNotice { get; set; }
        public string Privacy { get; set; }

        public FoundationHtmlFactory()
        {
            Email          = string.Empty;
            Linkedin       = string.Empty;
            Github         = string.Empty;
            Facebook       = string.Empty;
            Instagram      = string.Empty;
            Youtube        = string.Empty;
            LegalNotice    = string.Empty;
            Privacy        = string.Empty;
        }
        
        public string ResourcesPath
        {
            get
            {
                string? path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                return Path.Combine(path!, "FoundationResources");
            }
        }

        public IWebsite? Website { get; set; }

        public string MakeHeadHtml()
        {
            return "<link rel =\"stylesheet\" href=\"/foundation-theme/styles.css\">";
        }

        public string MakeIndexHtml(IWebsite website)
        {
            // Collect all items to show. 10 items max.
            List<IItem> items = new List<IItem>();
            foreach (ISection section in website.Sections)
            {
                section.Items.ForEach((item) => items.Add(item));
            }
            int showArticles = (items.Count > 10) ? 10 : items.Count;
            // http://procbits.com/2010/09/09/three-ways-to-sort-a-list-of-objects-with-datetime-in-c
            items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
            items.Reverse();
            items = items.GetRange(0, showArticles);

            return new Body().Add(new SiteHeader(website: website, email: Email, linkedin: Linkedin, github: Github, facebook: Facebook, instagram: Instagram, youtube: Youtube))
                                .Add(new Div()
                                    .Add(new Div(website.Index.Content)
                                            .Class("welcomeWrapper"))
                                    .Add(new H2("Latest Content"))
                                    .Add(new ItemList(items))
                                    .Class("wrapper"))
                                .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy))
                    .Render();
        }

        public string MakePageHtml(IPage page)
        {
            return new Body().Add(new SiteHeader(website: Website!, email: Email, linkedin: Linkedin, github: Github, facebook: Facebook, instagram: Instagram, youtube: Youtube))
                                .Add(new Div()
                                    .Add(new Article()
                                        .Add(new Div(page.Content)
                                            .Class("content")))
                                    .Class("wrapper"))
                                .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy))
                    .Render();
        }

        public string MakeSectionHtml(ISection section)
        {
            List<IItem> items = section.Items;
            items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
            items.Reverse();
            return new Body().Add(new SiteHeader(website: Website!, email: Email, linkedin: Linkedin, github: Github, facebook: Facebook, instagram: Instagram, youtube: Youtube))
                                .Add(new Div(section.Content)
                                    .Class("wrapper"))
                                .Add(new Div()
                                    .Add(new ItemList(items))
                                    .Class("wrapper"))
                                .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy))
                    .Render();
        }

        public string MakeItemHtml(IItem item)
        {
            return new Body().Add(new SiteHeader(website: Website!, email: Email, linkedin: Linkedin, github: Github, facebook: Facebook, instagram: Instagram, youtube: Youtube))
                                .Add(new Div()
                                    .Add(new TagList(item.Tags))
                                    .Add(new Text(item.Date.ToString("MMMM dd, yyyy")))
                                    .Class("item-meta-data-header"))
                                .Add(new Div()
                                    .Add(new Article()
                                        .Add(new Div(item.Content)
                                            .Class("content")))
                                    .Class("wrapper"))
                                .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy))
                    .Render();
        }

        public string MakeTagListHtml(List<IItem> items, string tag)
        {
            items.Sort((i1, i2) => DateTime.Compare(i1.Date.ToDateTime(TimeOnly.Parse("6pm")), i2.Date.ToDateTime(TimeOnly.Parse("6pm"))));
            items.Reverse();
            return new Body().Add(new SiteHeader(website: Website!, email: Email, linkedin: Linkedin, github: Github, facebook: Facebook, instagram: Instagram, youtube: Youtube))
                                .Add(new Div()
                                    .Add(new H1()
                                        .Add(new Text("Tagged with "))
                                        .Add(new bigTag(tag)))
                                    .Add(new ItemList(items))
                                    .Class("wrapper"))
                                .Add(new Footer(legalNotice: LegalNotice, privacy: Privacy))
                    .Render();
        }


        ////////////
        /// Custom Components
        ////////////

        private class SiteHeader : IHtmlComponent
        {
            private string _email;
            private string _linkedin;
            private string _github;
            private string _facebook;
            private string _instagram;
            private string _youtube;

            private List<string> _sections;
            private IWebsite _website;

            public SiteHeader(IWebsite website, string email, string linkedin, string github, string facebook, string instagram, string youtube)
            {
                _website = website;
                _sections = website.MakeSectionsFor;
                _email = email;
                _linkedin = linkedin;
                _github = github;
                _facebook = facebook;
                _instagram = instagram;
                _youtube = youtube;
            }
            public string Render()
            {
                Ul NavLinks = new();
                foreach (var section in _sections)
                {
                    if (section.ToString() is not null)
                    {
                        NavLinks.Add(new Li(new A(section).Href($"/{section}")));
                    }
                }
                return new Header(
                                new Div(
                                    new A(._website.Name).Href("/").Class("site-name")
                                ).Add(
                                    new Nav().Add(
                                        new Ul().Add(NavLinks)
                                    )
                                ).Class("wrapper")
                        )
                        .Add(new SocialIcons(email: _email, linkedin: _linkedin, github: _github, facebook: _facebook, instagram: _instagram, youtube: _youtube))
                        .Render();
            }
        }

        private class SocialIcons : IHtmlComponent
        {
            private string _email;
            private string _linkedin;
            private string _github;
            private string _facebook;
            private string _instagram;
            private string _youtube;

            public SocialIcons(string email, string linkedin, string github, string facebook, string instagram, string youtube)
            {
                _email = email;
                _linkedin = linkedin;
                _github = github;
                _facebook = facebook;
                _instagram = instagram;
                _youtube = youtube;
            }

            public string Render()
            {
                if (string.IsNullOrEmpty(_email) && string.IsNullOrEmpty(_linkedin) && string.IsNullOrEmpty(_github))
                {
                    return string.Empty;
                }

                var div = new Div();
                if (!string.IsNullOrEmpty(_email))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/mail.svg\">").Href($"mailto:{_email}"));
                }

                if (!string.IsNullOrEmpty(_linkedin))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/linkedin.svg\">").Href($"{_linkedin}"));
                }

                if (!string.IsNullOrEmpty(_github))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/github.svg\">").Href($"{_github}"));
                }

                if (!string.IsNullOrEmpty(_facebook))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/facebook.svg\">").Href($"{_facebook}"));
                }

                if (!string.IsNullOrEmpty(_instagram))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/instagram.svg\">").Href($"{_instagram}"));
                }

                if (!string.IsNullOrEmpty(_youtube))
                {
                    div.Add(new A("<img src=\"/foundation-theme/socialIcons/youtube.svg\">").Href($"{_youtube}"));
                }

                div.Class("social-icons");
                return div.Render();
            }
        }

        private class ItemList : IHtmlComponent
        {
            private List<IItem> items;
            public ItemList(List<IItem> items)
            {
                items = items;
            }
            public string Render()
            {
                var result = new Ul().Class("item-list");
                items.ForEach((item) => result.Add(
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

        public class TagList : IHtmlComponent
        {
            private List<string> tags;
            public TagList(List<string> tags)
            {
                tags = tags;
            }
            public string Render()
            {
                var result = new Ul().Class("tags");
                tags.ForEach((tag) => result.Add(
                                                new Li().Class("variant-default")
                                                        .Add(new A(tag).Href($"/tag/{tag}")))
                            );
                return result.Render();
            }
        }

        private class bigTag : IHtmlComponent
        {
            private string tag;
            public bigTag(string tag)
            {
                tag = tag;
            }
            public string Render()
            {
                var result = new Span(tag).Class("tag");
                return result.Render();
            }
        }

        private class Footer : IHtmlComponent
        {
            private string legalNotice;
            private string privacy;

            public Footer(string legalNotice, string privacy)
            {
                this.legalNotice = legalNotice;
                this.privacy = privacy;
            }

            public Footer()
            {
                this.legalNotice = string.Empty;
                this.privacy = string.Empty;
            }
            public string Render()
            {
                var footer = new StatiCsharp.HtmlComponents.Footer();
                footer.Add(new Paragraph()
                        .Add(new Text("Generated with ❤️ using "))
                        .Add(new A("StatiC#").Href("https://github.com/RolandBraunDev/StatiCsharp")));

                if (!string.IsNullOrEmpty(legalNotice) | !string.IsNullOrEmpty(privacy))
                {
                    var legal = new Paragraph();
                    
                    if (!string.IsNullOrEmpty(privacy))
                    {
                        legal.Add(new A("Datenschutz - Privacy").Href(privacy));
                    }

                    if (!string.IsNullOrEmpty(legalNotice))
                    {
                        legal.Add(new A("Impressum - Legal Notice").Href(legalNotice).Style("padding-left: 20px"));
                    }

                    footer.Add(legal);
                }

                return footer.Render();
            }
        }
    }
}