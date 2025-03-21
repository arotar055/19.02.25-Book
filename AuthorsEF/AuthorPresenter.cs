using System;

namespace AuthorsEF
{
    public class AuthorPresenter
    {
        private readonly Author auth;
        private readonly IAuthorView view;
        private readonly bool CheckNew;

        public AuthorPresenter(IAuthorView view, Author author, bool CheckiSNew)
        {
            this.view = view;
            CheckNew = CheckiSNew;
            auth = author;

            this.view.AuthorName = auth.Name;
            this.view.SetTitle(CheckNew ? "Add Author" : "Edit Author");
            this.view.SaveAuthor += OnSaveAuthor;
            this.view.Cancel += OnCancel;
        }

        private void OnSaveAuthor(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(view.AuthorName))
            {
                view.ShowMessage("Имя автора не может быть пустым");
                return;
            }
            auth.Name = view.AuthorName;
            view.SetOperationSuccess(true);
            view.CloseView();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            view.SetOperationSuccess(false);
            view.CloseView();
        }
    }
}