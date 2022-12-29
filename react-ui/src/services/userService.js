import httpRequest from "~/utilities/httpRequest";

const getProfile = (url) => {
  return httpRequest.get(url);
};

export { getProfile };
