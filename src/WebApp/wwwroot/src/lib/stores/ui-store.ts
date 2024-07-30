import { goto } from "$app/navigation";
import { get, writable } from "svelte/store";

export interface QueryParamatres {
  returnUrl?: string;
}

export interface User {
  displayName: string,
  login: string,
  email: string
}

export interface UiStore {
  queryParametres: QueryParamatres;
  user: User,
  loaded: boolean;
  url: string;
  isAuthorized: boolean;
}

const queryParamtresKeys = Object.keys({ returnUrl: "" } as QueryParamatres)

function createUiStore() {
  const P = writable<UiStore>({
    queryParametres: {},
    user: { displayName: "", email: "", login: "" },
    loaded: false,
    url: "/",
    isAuthorized: false
  });

  const { subscribe, update } = P;

  function init() {
    const split = window.location.href.split("?");

    const params = split.length > 1 ? split.slice(1).join("?") : undefined

    update(x => {
      x.url = window.location.pathname;

      if (params != undefined) {
        const splitParams = params.split('=');

        for (let i = 0; i + 1 < splitParams.length; i++) {
          if (queryParamtresKeys.includes(splitParams[i])) {
            (x.queryParametres as any)[splitParams[i]] = splitParams[i + 1];
          }
        }
      }

      return x;
    });
  }

  function getUrl(parametres?: QueryParamatres) {
    const { url } = get(P);


    return url + getQuery(parametres);
  }

  function getBackUrl(parametres?: QueryParamatres) {
    let { url } = get(P);

    return url.split("/").slice(0, -1).join("/") + getQuery(parametres);
  }

  function getNextUrl(additionalUrl: string, parametres?: QueryParamatres) {
    const { url } = get(P)

    return (url + additionalUrl).replace("//", "/") + getQuery(parametres);
  }

  function getQuery(parametres?: QueryParamatres) {
    let query = "";

    if (parametres != undefined) {
      const keys = Object.keys(parametres);

      for (let i = 0; i < keys.length; i++) {
        if (i == 0) {
          query += "?"
        }
        query += keys[i] + "=" + (parametres as any)[keys[i]]
        if (i != keys.length - 1) {
          query += "&";
        }
      }
    }

    return query;
  }

  function gotoBack(parametres?: QueryParamatres) {
    changeLocation(getBackUrl(), parametres);
  }

  function gotoNext(additionalUrl: string, parametres?: QueryParamatres) {
    changeLocation(getNextUrl(additionalUrl), parametres);
  }

  function changeLocation(url: string, parametres?: QueryParamatres) {
    update(x => {
      x.queryParametres = parametres ?? {};
      x.url = url;

      return x;
    });

    goto(url + getQuery(parametres));
  }

  return {
    init,
    getBackUrl,
    getUrl,
    getNextUrl,
    goto: changeLocation,
    gotoBack,
    gotoNext,
    update,
    subscribe,
  };
}

export const uiStore = createUiStore();
