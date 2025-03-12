export const formatDateForFilter = (value: string) => {
  if (!value) return "";

  if (value.includes("/")) {
    const [day, month, year] = value.split("/");
    return `${year}-${month}-${day}`;
  }
  return value;
};
