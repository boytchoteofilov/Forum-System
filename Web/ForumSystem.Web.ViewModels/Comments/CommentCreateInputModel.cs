﻿namespace ForumSystem.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CommentCreateInputModel
    {
        public int PostId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }
    }
}
