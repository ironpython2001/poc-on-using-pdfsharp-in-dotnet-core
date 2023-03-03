Useful Urls
1. https://github.com/ststeiger/PdfSharpNetStandard
2. https://gunnarpeipman.com/no-data-is-available-for-encoding/
3. https://chsamii.medium.com/no-data-is-available-for-encoding-1252-8bc14651d631
4. https://stackoverflow.com/questions/11429970/how-can-i-obtain-the-named-arguments-from-a-console-application-in-the-form-of-a


Note:
to resolve the error No data is available for encoding 1252
while calling the method doc.Save
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

reference 

https://chsamii.medium.com/no-data-is-available-for-encoding-1252-8bc14651d631

