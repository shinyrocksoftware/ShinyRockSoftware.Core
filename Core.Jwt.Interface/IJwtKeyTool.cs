using System.Security.Cryptography;
using Base.Model.Interface;

namespace Core.Jwt.Interface;

public interface IJwtKeyTool : IAutoInjection
{
    /// <summary>
    /// Symmetric decode token using secretKey in the configuration
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    IDictionary<string, string> SymmetricDecode(string token);

    /// <summary>
    /// Symmetric decode token with secretKey.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="secretKey"></param>
    /// <returns></returns>
    IDictionary<string, string> SymmetricDecode(string token, string secretKey);

    /// <summary>
    /// Symmetric encode token with secretKey in setting, support HS-family Algorithm.
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    string SymmetricEncode(IDictionary<string, object> payload, string algorithm);
    
    /// <summary>
    /// Symmetric encode token with secretKey in param.
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="secretKey"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    string SymmetricEncode(IDictionary<string, object> payload, string secretKey, string algorithm);

    /// <summary>
    /// Asymmetric encode with private key.
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="algorithm"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    string AsymmetricEncode(ECDsa privateKey, string algorithm, IDictionary<string, object> claims);
    
    /// <summary>
    /// Asymmetric decode with public key.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="publicKey"></param>
    /// <returns></returns>
    IDictionary<string, string> AsymmetricDecode(string token, ECDsa publicKey);
    
    /// <summary>
    /// Generate a pair of private and public keys.
    /// </summary>
    /// <param name="curve"></param>
    /// <returns></returns>
    (ECDsa privateKey, ECDsa publicKey) CreateAsymmetricKeys(ECCurve curve);
}