import httpRequest, { authHttpRequest } from "~/utilities/httpRequest";

const getProfile = (url) => {
  return authHttpRequest.get(url);
};

export { getProfile };
