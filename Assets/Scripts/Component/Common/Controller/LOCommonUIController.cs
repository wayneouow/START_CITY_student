using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Controller {

    public interface ILOCommonUIController {

        ILODialogController DialogController { get; }
    }

    public class LOCommonUIController : ILOCommonUIController {

        public static ILOCommonUIController Create() {

            return new LOCommonUIController();
        }

        public ILODialogController DialogController { get; private set; }

        private LOCommonUIController() {

            DialogController = LODialogController.Create();
        }
    }
}