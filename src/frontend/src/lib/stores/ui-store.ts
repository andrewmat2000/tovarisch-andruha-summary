import { goto } from "$app/navigation";
import { writable } from "svelte/store";

export interface UiStore {
    url: string;
}

function createUiStore() {
    const P = writable<UiStore>({
        url: window.location.pathname
    });

    const { subscribe, update } = P;

    function changeLocation(url: string) {
        goto(url);
        update(x => { x.url = url; return x; });
    }



    return {
        goto: changeLocation,
        subscribe
    }
}

export const uiStore = createUiStore();