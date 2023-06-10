namespace prenotazioni_postazioni_api.Exceptions
{
    [Serializable]
    public class PrenotazionePostazioniApiException : Exception
    {
        public PrenotazionePostazioniApiException() : base() { }
        
        public PrenotazionePostazioniApiException(string message) : base(message) { }
        
        public PrenotazionePostazioniApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}
