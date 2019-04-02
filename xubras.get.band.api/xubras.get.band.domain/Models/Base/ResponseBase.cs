using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace xubras.get.band.domain.Models.Base
{
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
            Errors = new List<ErrorResponseBase>();
        }

        protected IList<ErrorResponseBase> Errors { get; private set; }

        public void AddError(ValidationResult validation)
        {
            foreach (ValidationFailure item in validation.Errors)
                Errors.Add(new ErrorResponseBase { Property = item.PropertyName, Message = item.ErrorMessage });
        }

        public bool IsValid()
        {
            return !Errors.Any();
        }

        public IList<ErrorResponseBase> GetErrors()
        {
            return this.Errors;
        }
    }
}