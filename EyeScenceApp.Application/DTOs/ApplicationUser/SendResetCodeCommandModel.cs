
using System.ComponentModel.DataAnnotations;

public class SendResetCodeCommandModel
{
    [Required(ErrorMessage = "Email Is Required here , please Add a Valid Email.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}