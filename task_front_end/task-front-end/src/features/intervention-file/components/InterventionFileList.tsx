import { SearchField } from "../../../shared/components/SearchField";
import { DateField } from "../../../shared/components/DateField";
import { Pagination } from "../../../shared/components/Pagination";
import { useInterventionFileListContext } from "../hooks/useInterventionFileListContext";
import { Button } from "../../../shared/components/Button";
import { InterventionFileDetailsModal } from "./InterventionFileDetailsModal";
import { TablePlaceholder } from "../../../shared/components/TablePlaceholder";

export const InterventionFileList = () => {
  const {
    isLoading,
    currentPage,
    totalPages,
    setCurrentPage,
    interventions,
    selectedIntervention,
    setSelectedIntervention,
    handleNewIntervention,
    handleRowClick,
    handleUpdateIntervention,
    searchFilters,
    handleSearch,
  } = useInterventionFileListContext();

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Intervention Files</h2>
        <Button onClick={handleNewIntervention}>New Intervention</Button>
      </div>

      <div className="row d-flex justify-content-center mb-4">
        <div className="col-md-3 mb-2">
          <SearchField
            value={searchFilters.clientName}
            onChange={(value) => handleSearch("clientName", value)}
            placeholder="Search Customer..."
          />
        </div>
        <div className="col-md-3 mb-2">
          <SearchField
            value={searchFilters.technicianName}
            onChange={(value) => handleSearch("technicianName", value)}
            placeholder="Search Technician..."
          />
        </div>
        <div className="col-md-3 mb-2">
          <DateField
            value={searchFilters.interventionDate}
            onChange={(value) => handleSearch("interventionDate", value)}
          />
        </div>
      </div>

      <div style={{ height: "30rem", overflow: "auto" }}>
        <table
          className="table table-striped table-hover"
          style={{ tableLayout: "fixed" }}
        >
          <thead
            style={{
              position: "sticky",
              top: 0,
              backgroundColor: "white",
              zIndex: 1,
            }}
          >
            <tr>
              <th style={{ width: "12rem" }}>Date</th>
              <th style={{ width: "12rem" }}>Technician</th>
              <th style={{ width: "12rem" }}>Customer</th>
              <th style={{ width: "12rem" }}>Description</th>
              <th style={{ width: "12rem" }}>Status</th>
            </tr>
          </thead>
          <tbody>
            {isLoading ? (
              <TablePlaceholder colSpan={5} rows={10} />
            ) : (
              interventions.map((item) => (
                <tr
                  key={item.id}
                  onClick={() => handleRowClick(item)}
                  style={{ cursor: "pointer" }}
                >
                  <td style={{ width: "12rem" }}>
                    {new Date(item.interventionDate).toLocaleDateString()}
                  </td>
                  <td style={{ width: "12rem" }}>{item.technicianName}</td>
                  <td style={{ width: "12rem" }}>{item.clientName}</td>
                  <td style={{ width: "12rem" }}>{item.workPointAddress}</td>
                  <td style={{ width: "12rem" }}>{item.status}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      <div className="d-flex justify-content-center mt-4">
        <Pagination
          currentPage={currentPage}
          pageCount={totalPages}
          onPageChange={setCurrentPage}
        />
      </div>

      <InterventionFileDetailsModal
        intervention={selectedIntervention}
        onClose={() => setSelectedIntervention(null)}
        onUpdate={handleUpdateIntervention}
      />
    </div>
  );
};
