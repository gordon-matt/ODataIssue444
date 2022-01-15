This repo's purpose is to show the problem with OData functions not working when updating to .NET 6 / OData 8.

See issue: https://github.com/OData/AspNetCoreOData/issues/444

```
.NET 5: `master` branch (works fine)

.NET 6: `develop` branch (broken)
```

Try one of these test URLs:

```
https://localhost:44306/odata/LocalizableStringApi/Default.GetComparitiveTable(cultureCode='en-US')

https://localhost:44306/odata/LocalizableStringApi/Default.GetComparitiveTable(cultureCode='jp-JP')
```

You will get a 404 in the .NET 6 version
