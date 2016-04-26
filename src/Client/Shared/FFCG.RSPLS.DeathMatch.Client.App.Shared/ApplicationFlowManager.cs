namespace FFCG.RSPLS.DeathMatch.Client.App.Shared
{
    public class ApplicationFlowManager
    {
        private readonly IPresentationManager _presentationManager;

        public ApplicationFlowManager(IPresentationManager presentationManager)
        {
            _presentationManager = presentationManager;
        }

        public void Initialize()
        {
            _presentationManager.ShowView(Views.Start);
        }
    }
}