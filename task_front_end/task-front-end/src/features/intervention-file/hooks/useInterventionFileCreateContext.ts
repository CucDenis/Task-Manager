import { useContext } from "react";
import { InterventionFileCreateFormContext } from "../context/InterventionFileCreateForm/InterventionFileCreateFormContext";

export const useInterventionFileCreateContext = () => {
  return useContext(InterventionFileCreateFormContext);
};
