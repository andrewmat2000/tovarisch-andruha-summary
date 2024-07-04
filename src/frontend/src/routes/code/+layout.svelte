<script>
  import { githubDark } from "svelte-highlight/styles";
</script>

<svelte:head>
  {@html githubDark}
</svelte:head>

<slot />
