import { useState, FC, ReactNode } from "react";
import { InterventionFileContainerContext } from "./InterventionFileContainerContext";

export const InterventionFileContainerProvider: FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [loading, setLoading] = useState(false);

  const contextValue = {
    loading,
    setLoading,
  };

  return (
    <InterventionFileContainerContext.Provider value={contextValue}>
      {children}
    </InterventionFileContainerContext.Provider>
  );
};
