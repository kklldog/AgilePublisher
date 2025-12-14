namespace AgilePublisher.Models;

public record ZhihuSelectors(
    string EditorUrl,
    string TitleInput,
    string BodyEditable,
    string TagInput,
    string CoverUploader,
    string PublishButton,
    string DraftButton,
    string ConfirmPublishButton)
{
    public static ZhihuSelectors Default => new(
        EditorUrl: "https://zhuanlan.zhihu.com/write",
        TitleInput: "textarea.NotionInput",
        BodyEditable: "div.NotionEditor",
        TagInput: "[data-testid='ArticleTagsEditor'] input",
        CoverUploader: "input[type='file'][accept='image/*']",
        PublishButton: "button:has-text('发布')",
        DraftButton: "button:has-text('存为草稿')",
        ConfirmPublishButton: "button:has-text('确认并发布')");
}
