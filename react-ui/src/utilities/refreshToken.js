import { localStoredKey } from "~/enums/localStoredKey";
import { removeToken } from "./localStoredManager";
import { refreshAccessToken } from "~/services/authService";
import { getToken, setToken } from "./localStoredManager";
import { apiStatus } from "~/enums/apiStatus";

export const refreshToken = async () => {
  const { accessToken = null, refreshToken = null } = JSON.parse(
    getToken(localStoredKey.token)
  );
  try {
    const { status, data } = await refreshAccessToken("User/RefreshToken", {
      accessToken: accessToken,
      refreshToken: refreshToken,
    });

    if (status === apiStatus.success) {
      setToken(localStoredKey.token, JSON.stringify(data));
    }
    return data;
  } catch (error) {
    console.log("error", error);
    if(error.request.responseURL.includes('RefreshToken')){
      window.location.href = '/sign-in';
    }
  }
};
