using System.Collections.Generic;

namespace AsteroidsProject.Shared
{
    public class SceneConfig
    {
        public string SceneLabel { get; set; }
        public string PreLoadAssetLabel { get; set; }
        public string PreInitUILabel { get; set; }
        public List<string> EcsUpdateSystems { get; set; }
        public List<string> EcsFixedUpdateSystems { get; set; }
        public List<string> EcsGUISystems { get; set; }
    }
}