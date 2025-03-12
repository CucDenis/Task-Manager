/* eslint-disable @typescript-eslint/no-explicit-any */
export const mockInterventions: any[] = Array.from(
  { length: 25 },
  (_, index) => ({
    id: index + 1,
    date: new Date(2024, 0, 15 + (index % 14)).toISOString().split("T")[0],
    technician: [
      "John Doe",
      "Jane Smith",
      "Mike Johnson",
      "Sarah Wilson",
      "Bob Miller",
    ][index % 5],
    customer: [
      "ABC Company",
      "XYZ Corp",
      "123 Industries",
      "Tech Solutions",
      "Global Systems",
    ][index % 5],
    description: [
      "Router maintenance",
      "Network setup",
      "Server repair",
      "Security update",
      "Fiber installation",
      "Wireless setup",
      "Hardware replacement",
      "Software update",
      "Database backup",
      "Cloud migration",
    ][index % 10],
    status: ["Completed", "Pending", "In Progress", "Scheduled", "Cancelled"][
      index % 5
    ],
  })
);
