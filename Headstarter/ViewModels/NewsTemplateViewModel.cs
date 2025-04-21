using System.ComponentModel;
using System.Windows.Input;

namespace Headstarter.ViewModels;

public class NewsTemplateViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    private string title;
    public string Title
    {
        get => title;
        private set
        {
            if (title != value)
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }

    private string description;
    public string Description
    {
        get => description;
        private set
        {
            if (description != value)
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    private string image;
    public string Image
    {
        get => image;
        private set
        {
            if (image != value)
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }
    private string content;
    public string Content
    {
        get => content;
        private set
        {
            if (content != value)
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
    }
    public NewsTemplateViewModel(string content, string image, string description, string title)
    {
        Content = content;
        Image = image;
        Description = description;
        Title = title;
    }
}