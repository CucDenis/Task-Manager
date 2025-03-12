import { useContext } from "react";
import { InterventionFileContainerContext } from "../context/InterventionFileContainer/InterventionFileContainerContext";

export const useInterventionFileContainerContext = () => {
  return useContext(InterventionFileContainerContext);
};
