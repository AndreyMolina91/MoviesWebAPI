using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movies.Utilities.Validations
{
    public class FileTypeValidate : ValidationAttribute
    {
        //Herramienta Array de strings que contendrá el tipo de archivo valido .jpg .png, etc
        public readonly string[] _typeValidate;
        public FileTypeValidate(string[] typeValidate)
        {
            _typeValidate = typeValidate;
        }
        //Pasamos el enum FileTypeGroup por parametro 
        //Constructor con la clase filetypegroup por parametro para acceder al enum con image
        public FileTypeValidate(FileTypeGroup fileTypeGroup)
        {
            //si el enum es igual a Imagen se aceptan los archivos con extension...
            if (fileTypeGroup == FileTypeGroup.Image)
            {
                //jpeg, png, gif ... Ahora poner validacion en la propiedad del Dto
                _typeValidate = new string[] {"image/jpeg","image/png","image/gif"};
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile; //Valor como un FormFile

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            //Si el TypeValidate que contiene los jpeg, png, gif es diferente de formfile.ContenType lanza la validación 
            //sino lanza el returno success
            if (!_typeValidate.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Tipos de archivo aceptados: {string.Join(",", _typeValidate)} ");
            }

            return ValidationResult.Success;
        }
    }
}
