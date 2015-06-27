# Office Apps (又称为Apps for Office，Apps for Office，Office 2013 Add-Ins) 开发 － 中文文档计划
##项目背景
继 [VBA](http://simpeng.net/oai/oai-chapter-1/compare-office-addin-with-vba-and-vsto.html?s=oai-chapter-1-section-4  了解VBA) 和 [VSTO](http://simpeng.net/oai/oai-chapter-1/compare-office-addin-with-vba-and-vsto.html?s=oai-chapter-1-section-4 了解VSTO) 之后， 微软为 Office 平台开发者提供了新的扩展应用模型（App Model)，基于此平台实现的 Office 扩展应用，官方称之为 Office Apps，也称为 Apps for Office 或 Office 2013 Add-Ins。相对于 VBA 和 VSTO 两种开发方式，新的 App Model 有如下几点不同：
* ［传播与分享］VBA 和 VSTO 的开发者大多是为了提高工作效率，在 Office 中定制部门或公司内部的特定功能，开发出的脚本或者Assembly多在本部门或者公司内传播使用。App Model 允许开发者将应用发布在 Office 商店，使全球的 Office 用户可以使用它，并且允许开发者根据需求定义应用是否收费。这将大大地减少不同公司不同部门之间的重复投资，使“共享同一个应用完成同一类事情”变成可能。
* ［学习门槛与技术延续性］VBA 和 VSTO 的开发者需要学习较多的入门知识，才能弄清两种开发模型与 Office 程序的交互流程，有较高的学习门槛。比如需要弄清 VBA 有哪些语法，可以做什么程度的事情。App Model 的开发更多地像是 Web 应用程序的开发，开发者可以将 Web 应用的开发体验无缝地迁移到 Office 扩展应用开发中。开发中唯一需要额外了解的是如何利用 App Model 提供的 JSOM API 与文档进行交互。
* ［扩展程序的更新］基于 VBA 和 VSTO 的 Office 扩展应用程序（或者脚本），一般时通过本地拷贝的方式进行传播，它们运行在本地的 Office 软件并依赖于相应的平台支持（如 VSTO 依赖于 Office 软件提供的主互操作程序集，诸如Microsoft.Office.Interop.Excel.dll）。 然而当 Office 软件或一些平台支持不一致时，会出现同一个脚本或应用在不同的机器上表现不同，甚至是不支持。新的 App Model下开发的 Office App， 由于本质上是运行在 Office 平台（包括2013及以后的富客户端，Web 版， RT版，甚至是 Mac版 [详情请点击此处](http://simpeng.net/oai/oai-chapter-1/office-addin-types-and-current-platoform-supporting.html?s=oai-chapter-1-section-)）上嵌入的的 iframe 中，而 App 本身是被开发者 host 在远端的 Web 服务器上。 App 开发者可以根据情况快速的更新 App 的功能或者修正 bug。
* ［开放的模型提供更多的可能性］新的 App Model 使得 Office 软件变的更加开放，开发者可以将 Office 平台强大的文档功能与开放的互联网技术连接在一起，开发者可以在 App 中做到 VBA 脚本，基于 VSTO 的程序做不到的事情。比如，越来越多的数据源提供给开发者 REST API， 其中 Office 365 REST API 就允许开发者通过一组 URL的方式访问用户的邮件，日历，SharePoint Online 和 OneDrive for Business 上的目录及文件等等，开发者可以在应用中利用灵活的 Web 开发技术简单高效的访问不同的数据源，达到自己的目的。

##新 App 的名称
这里我们讲在新的 App Model 下开发的 App 翻译为“Office 扩展应用”，而在这之前，它还先后被称为
Office 2013 Apps，Apps for Office，Office 2013 Add-Ins（区别与使用 VSTO 构建的 Office Add-ins）。

##为何提供非官方的中文文档
* 首先，微软 Office 官网[dev.office.com](https://dev.office.com) 上提供的关于新的 App Model 的文档不够 Agile。很多开发者喜欢通过简单的例子了解开发平台的工作模型，基于开发平台可以接触哪些数据源，能够做什么程度的操作。这样可以快速的评估开发平台能否满足自己的应用开发需求。
* 第二，微软官方文档的中文翻译不够精确，不能很好的让中国开发者读懂。
* 第三，希望提供一些更多可用并可运行的例子，帮助开发者更快的了解应用平台。
* 最后，通过 GitHub 平台，让更多的有需求在 Office 平台上构建 App 的开发者高效的完成自己的目标。

##在线文档地址
放在 GitHub 上的内容包括两部分：
* 通过 XML 文件生成 HTML 网页的 C# 程序；
* 由上述程序生成的 HTML 文档。
这些 HTML 文档将 host 在 [http://simpeng.net/oai](http://simpeng.net/oai) ，如有变动，将会在此更新。

本项目旨在提供中文文档帮助，为开发者扫清认识新的Office App模型中的障碍。
