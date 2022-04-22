import axios from "axios";

class callAPI {
  static baseURL = "https://localhost:44301/";

  getAuth() {
    if (localStorage.getItem("TokenBarrier")?.length == null) {
      const config = {
        headers: {
          Accept: "application/json",
        },
      };
      return config;
    } else {
      const config = {
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + localStorage.getItem("TokenBarrier"),
        },
      };
      return config;
    }
  }

  static async AsyncGET(url: string) {
    return await (
      await axios.get(this.baseURL + url, new callAPI().getAuth())
    ).data;
  }

  static async AsyncPOST(url: string, payload: any) {
    return await (
      await axios.post(this.baseURL + url, payload, new callAPI().getAuth())
    ).data;
  }
}

export default callAPI;
