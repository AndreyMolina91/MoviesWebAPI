using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Utilities.CustomModelBinder
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //Nombre de nuestra propiedad 
            var propertyName = bindingContext.ModelName;
            //Proveedor del valor
            var valuesProvider = bindingContext.ValueProvider.GetValue(propertyName);
            //Si el valor es nulo o no existe pues terminamos la tarea y no hace nada
            if (valuesProvider == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            //Si existe entonces...
            try
            {
                //Deserealizamos el valor con newtonsoft mediante el generico lo aplicamos a cualquier modelo
                var deserializedValue = JsonConvert.DeserializeObject<T>(valuesProvider.FirstValue);
                //pasamos el resultado como deserealizado exitoso en este caso sera el listado de ids 
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {
                //Enviamos en el nombre de la propiedad var, el error
                bindingContext.ModelState.TryAddModelError(propertyName, "Valor invalido para tipo List<int>");
            }

            return Task.CompletedTask;
        }
    }
}
