import axios from "axios";

class callAPI {
  static baseURL = "https://localhost:5001/";

  getAuth() {
    if (localStorage.getItem("logintoken")?.length == null) {
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
          Authorization: "Bearer " + localStorage.getItem("logintoken"),
        },
      };
      return config;
    }
  }

  static async AsyncGET(url) {
    return await (
      await axios.get(this.baseURL + url, new callAPI().getAuth())
    ).data;
  }

  static async AsyncPOST(url, payload) {


    return await (
      await axios.post(this.baseURL + url, payload, new callAPI().getAuth())
    ).data;
  }
}

export default callAPI;
