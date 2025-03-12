/* eslint-disable @typescript-eslint/no-unused-vars */
import { FC, ReactNode, useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { InterventionFileListContext } from "./InterventionFileListContext";
import {
  useAppDispatch,
  useAppSelector,
} from "../../../../app/redux/reduxHooks";
import {
  fetchInterventions,
  selectInterventions,
  selectInterventionPagination,
  selectInterventionLoading,
} from "../../slice/interventionSlice";
import { formatDateForFilter } from "../../../../shared/utils/functions";

export const InterventionFileListProvider: FC<{ children: ReactNode }> = ({
  children,
}) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const [currentPage, setCurrentPage] = useState(1);
  const [search, setSearch] = useState("");
  const [filters, setFilters] = useState({
    technician: "",
    customer: "",
    date: "",
  });
  const [selectedIntervention, setSelectedIntervention] =
    useState<InterventionFile | null>(null);

  const [searchFilters, setSearchFilters] = useState({
    technicianName: "",
    clientName: "",
    interventionDate: "",
  });

  const interventions = useAppSelector(selectInterventions);
  const { totalPages } = useAppSelector(selectInterventionPagination);
  const isLoading = useAppSelector(selectInterventionLoading);

  const debouncedSearch = useCallback(
    (filters: typeof searchFilters) => {
      dispatch(
        fetchInterventions({
          pageNumber: currentPage,
          pageSize: 10,
          ...filters,
        })
      );
    },
    [dispatch, currentPage]
  );

  useEffect(() => {
    const timer = setTimeout(() => {
      debouncedSearch(searchFilters);
    }, 500);

    return () => clearTimeout(timer);
  }, [searchFilters, debouncedSearch]);

  const handleSearch = (key: keyof typeof searchFilters, value: string) => {
    setSearchFilters((prev) => ({
      ...prev,
      [key]: key === "interventionDate" ? formatDateForFilter(value) : value,
    }));
    setCurrentPage(1);
  };

  const handleNewIntervention = () => {
    navigate("/interventions/new");
  };

  const handleRowClick = (intervention: InterventionFile) => {
    setSelectedIntervention(intervention);
  };

  const handleUpdateIntervention = async (
    updatedIntervention: InterventionFile
  ) => {
    try {
      // Add your API call here
      await new Promise((resolve) => setTimeout(resolve, 1000));
      dispatch(fetchInterventions({ pageNumber: currentPage }));
    } catch (error) {
      console.log(updatedIntervention);
      console.error("Failed to update intervention:", error);
    }
  };

  const contextValue = {
    currentPage,
    setCurrentPage,
    search,
    setSearch,
    filters,
    setFilters,
    interventions,
    totalPages,
    selectedIntervention,
    setSelectedIntervention,
    handleNewIntervention,
    handleRowClick,
    handleUpdateIntervention,
    isLoading,
    searchFilters,
    handleSearch,
  };

  return (
    <InterventionFileListContext.Provider value={contextValue}>
      {children}
    </InterventionFileListContext.Provider>
  );
};
