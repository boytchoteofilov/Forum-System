namespace ForumSystem.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent =>
            this.Content?.Length > 50
            ? this.Content.Substring(0, 50) + "..."
            : this.Content;

        // from identity
        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }
    }
}
