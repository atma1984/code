using Packt.Shared;
using static System.Console; // Для удобства использования методов Console (Write, WriteLine и т.д.)


Write("Enter some text to sign: ");
string? data = ReadLine();
string signature = Protector.GenerateSignature(data);
WriteLine($"Signature: {signature}");
WriteLine("Public key used to check signature:");
WriteLine(Protector.PublicKey);
if (Protector.ValidateSignature(data, signature))
{
    WriteLine("Correct! Signature is valid.");
}
else
{
    WriteLine("Invalid signature.");
}
// имитируем поддельную подпись, заменив первый символ на X или Y
string fakeSignature = signature.Replace(signature[0], 'X');
if (fakeSignature == signature)
{
    fakeSignature = signature.Replace(signature[0], 'Y');
}
if (Protector.ValidateSignature(data, fakeSignature))
{
    WriteLine("Correct! Signature is valid.");
}
else
{
    WriteLine($"Invalid signature: {fakeSignature}");
}