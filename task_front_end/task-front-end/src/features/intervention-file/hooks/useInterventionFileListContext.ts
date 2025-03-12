import { useContext } from "react";
import { InterventionFileListContext } from "../context/InterventionFileList/InterventionFileListContext";

export const useInterventionFileListContext = () => {
  return useContext(InterventionFileListContext);
};
