using AIEnterpriseOS.DocumentAI.Service.Models;
using AIEnterpriseOS.DocumentAI.Service.OCR;
using AIEnterpriseOS.DocumentAI.Service.Classifiers;
using AIEnterpriseOS.DocumentAI.Service.Extractors;

namespace AIEnterpriseOS.DocumentAI.Service.Services;

public interface IDocumentEngine
{
    DocumentAnalysis Process(byte[] file);
    IEnumerable<DocumentAnalysis> All();
}

public class InMemoryDocumentEngine : IDocumentEngine
{
    private readonly IOcrEngine _ocr;
    private readonly IDocumentClassifier _classifier;
    private readonly IFieldExtractor _extractor;

    private readonly List<DocumentAnalysis> _docs = new();

    public InMemoryDocumentEngine(IOcrEngine ocr, IDocumentClassifier classifier, IFieldExtractor extractor)
    {
        _ocr = ocr;
        _classifier = classifier;
        _extractor = extractor;
    }

    public DocumentAnalysis Process(byte[] file)
    {
        var text = _ocr.ExtractText(file);
        var type = _classifier.Classify(text);
        var fields = _extractor.Extract(type, text);

        var doc = new DocumentAnalysis
        {
            Type = type,
            RawText = text,
            Fields = fields
        };

        _docs.Add(doc);
        return doc;
    }

    public IEnumerable<DocumentAnalysis> All() => _docs;
}
