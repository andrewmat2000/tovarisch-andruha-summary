<script lang="ts">
  import { goto } from "$app/navigation";

  export let link: string;
  export let label: string;

  function go() {
    goto(link);
  }
</script>

<input type="button" on:click={go} value={label} />
