using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.Models;

public class SourceModel
{
	public string ClientNamespace { get; set; }
	public string ClassModifier { get; set; }
	public string Namespace { get; set; }
}