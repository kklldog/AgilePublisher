# Agile Publisher

使用 C# 和 Playwright 自动化将 Markdown 文章发布到内容平台，当前支持 **知乎**（Zhuanlan）。设计上保持可扩展，后续可以添加微信公众号等新平台。

## 功能特性
- 通过 Playwright 模拟浏览器完成文章发布或保存草稿。
- 提供命令行参数以指定标题、封面、标签以及发布/草稿模式。
- Playwright 会话配置支持 headless/headful、慢速模式和持久化用户目录，方便跳过重复登录。
- 平台可扩展：实现新的 `IPublisher` 并注册即可支持更多站点。

## 环境要求
- .NET 8 SDK
- 已安装的 Playwright Chromium 浏览器依赖（首次运行后 Playwright 会自动下载）

## 构建与运行
```bash
# 还原依赖（需要 .NET SDK）
dotnet restore src/AgilePublisher/AgilePublisher.csproj

# 使用命令行发布或保存草稿
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 dotnet run --project src/AgilePublisher -- \
  --platform zhihu \
  --markdown ./post.md \
  --title "我的文章标题" \
  --tag csharp --tag playwright \
  --cover-image ./cover.png \
  --user-data-dir ./playwright-profile \
  --publish
```

常用参数：
- `--markdown`：要发布的 Markdown 文件路径（必填）。
- `--title`：可选，覆盖 Markdown 中的标题。
- `--cover-image`：封面图片路径。
- `--tag`：多次传入可添加多个标签。
- `--publish`：直接发布；不传则默认保存为草稿。
- `--user-data-dir`：Playwright 持久化目录，用于保持登录状态。
- `--headless false`：需要调试时关闭 headless。

## 扩展到其他平台
1. 在 `Publishers` 命名空间下实现新的 `IPublisher`，参照 `ZhihuPublisher`。
2. 在 `PublisherService` 的注册表中添加新的平台键值。
3. 根据目标站点的选择器和流程补充具体的交互逻辑。
