using System;
using Member.Common.Constants;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Google.Protobuf;


namespace Member.Common.Helper
{
    public class ValidationObject<T>
    {
        private readonly T _entity;
        

        public ValidationObject(T entity)
        {
            _entity = entity;
        }

        public ValidationObject<T> IsNull()
        {
            if (_entity == null)
                throw new Exception();

            return this;
        }
        public ValidationObject<T> isUniqe(bool b)
        {
            if (b)
            {
                throw new Exception();
            }
            return this;
        }
        public ValidationObject<T> IsEmpty(string? message = null)
        {
            if (string.IsNullOrEmpty(_entity?.ToString())
                   || string.IsNullOrWhiteSpace(_entity?.ToString())) throw new ValidationException(message);

            return this;
        }

        public ValidationObject<T> RegexValidation(string regexString, string? message = null)
        {
            Regex regex = new Regex(regexString);
            if (!regex.IsMatch(Convert.ToString(_entity)))
                throw new ValidationException(message);

            return this;
        }
       
    }
    public static class Validate
    {
        public static ValidationObject<T> For<T>(T entity)
        {
            return new ValidationObject<T>(entity);
        }
    }
}

