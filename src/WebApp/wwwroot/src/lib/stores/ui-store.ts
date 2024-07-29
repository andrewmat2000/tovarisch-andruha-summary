import { goto } from "$app/navigation";
import { get, writable } from "svelte/store";

export interface UiStore {
  url: string;
}

function createUiStore() {
  const P = writable<UiStore>({
    url: window.location.pathname,
  });

  const { subscribe, update } = P;

  function changeLocation(url: string) {
    goto(url);
    update(x => {
      x.url = url;
      return x;
    });
  }

  function goBack() {
    let { url } = get(P);

    url = url.split("/").slice(0, -1).join("/");

    changeLocation(url.length > 0 ? url : "/");
  }

  return {
    goBack,
    goto: changeLocation,
    subscribe,
  };
}

export const uiStore = createUiStore();
