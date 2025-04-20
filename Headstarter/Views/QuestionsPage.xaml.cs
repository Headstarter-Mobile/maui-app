namespace Headstarter.Views;

public partial class QuestionsPage : ContentPage
{
	public QuestionsPage()
	{
		InitializeComponent();
	}

    //vupros 1
    private void OnFrameTapped1(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel1.IsVisible;

        AnswerLabel1.IsVisible = isExpanding;

        IconLabel1.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame1.Animate("Expand", new Animation(v => QuestionFrame1.HeightRequest = v, QuestionFrame1.Height, 250), 16, duration);
        }
        else
        {
            QuestionFrame1.Animate("Collapse", new Animation(v => QuestionFrame1.HeightRequest = v, QuestionFrame1.Height, 120), 16, duration);
        }
    }

    //vupros 2
    private void OnFrameTapped2(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel2.IsVisible;

        AnswerLabel2.IsVisible = isExpanding;

        IconLabel2.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame2.Animate("Expand", new Animation(v => QuestionFrame2.HeightRequest = v, QuestionFrame2.Height, 250), 16, duration);
        }
        else
        {
            QuestionFrame2.Animate("Collapse", new Animation(v => QuestionFrame2.HeightRequest = v, QuestionFrame2.Height, 120), 16, duration);
        }
    }

    //vupros 3
    private void OnFrameTapped3(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel3.IsVisible;

        AnswerLabel3.IsVisible = isExpanding;

        IconLabel3.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame3.Animate("Expand", new Animation(v => QuestionFrame3.HeightRequest = v, QuestionFrame3.Height, 270), 16, duration);
        }
        else
        {
            QuestionFrame3.Animate("Collapse", new Animation(v => QuestionFrame3.HeightRequest = v, QuestionFrame3.Height, 120), 16, duration);
        }
    }

    //vupros 4
    private void OnFrameTapped4(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel4.IsVisible;

        AnswerLabel4.IsVisible = isExpanding;

        IconLabel4.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame4.Animate("Expand", new Animation(v => QuestionFrame4.HeightRequest = v, QuestionFrame4.Height, 190), 16, duration);
        }
        else
        {
            QuestionFrame4.Animate("Collapse", new Animation(v => QuestionFrame4.HeightRequest = v, QuestionFrame4.Height, 120), 16, duration);
        }
    }

    //vupros 5
    private void OnFrameTapped5(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel5.IsVisible;

        AnswerLabel5.IsVisible = isExpanding;

        IconLabel5.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame5.Animate("Expand", new Animation(v => QuestionFrame5.HeightRequest = v, QuestionFrame5.Height, 210), 16, duration);
        }
        else
        {
            QuestionFrame5.Animate("Collapse", new Animation(v => QuestionFrame5.HeightRequest = v, QuestionFrame5.Height, 120), 16, duration);
        }
    }

    //vupros 6
    private void OnFrameTapped6(object sender, EventArgs e)
    {
        bool isExpanding = !AnswerLabel6.IsVisible;

        AnswerLabel6.IsVisible = isExpanding;

        IconLabel6.Text = isExpanding
            ? (string)Application.Current.Resources["IconMinusBox"]
            : (string)Application.Current.Resources["IconPlusBox"];

        uint duration = 250;

        if (isExpanding)
        {
            QuestionFrame6.Animate("Expand", new Animation(v => QuestionFrame6.HeightRequest = v, QuestionFrame6.Height, 190), 16, duration);
        }
        else
        {
            QuestionFrame6.Animate("Collapse", new Animation(v => QuestionFrame6.HeightRequest = v, QuestionFrame6.Height, 120), 16, duration);
        }
    }

}