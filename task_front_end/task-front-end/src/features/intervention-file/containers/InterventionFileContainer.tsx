import { useEffect } from "react";
import { InterventionFileContainerProvider } from "../context/InterventionFileContainer/InterventionFileContainerProvider";
import { InterventionFileList } from "../components/InterventionFileList";
import { useInterventionFileContainerContext } from "../hooks/useInterventionFileContainerContext";
import { InterventionFileListProvider } from "../context/InterventionFileList/InterventionFileListProvider";

const InterventionFileContainerInner = () => {
  const { setLoading } = useInterventionFileContainerContext();

  useEffect(() => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
    }, 1000);
  }, [setLoading]);

  return (
    <InterventionFileListProvider>
      <InterventionFileList />
    </InterventionFileListProvider>
  );
};

export const InterventionFileContainer = () => {
  return (
    <InterventionFileContainerProvider>
      <InterventionFileContainerInner />
    </InterventionFileContainerProvider>
  );
};
