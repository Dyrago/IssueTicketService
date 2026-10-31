<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';
	import '../lib/styles/styles.css'
	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<div>
	<header>
		<a href="/"><img src="../src/lib/assets/favicon.svg" alt="Icon"></a>
	</header>

	{@render children()}

	<footer id="footer">
		Created by ®Dyrago
	</footer>
</div>

<style>

	header{
		margin-bottom: 1rem;
		border: 1px solid black;
		padding: 1rem;
		color: #fff;
		background-color: rgb(28, 28, 221);
		display: grid;
		grid-template-columns: repeat(12, 1fr);
	}

	nav{
		grid-column: 12/13;
		display: flex;
		gap: 0.5rem;
		flex-direction: column;
		justify-content: center;
	}

	nav > a > button{
        border: 1px solid black;
        border-radius: 10px;
        text-decoration: none;
        background-color: orangered;
        padding: 0.5rem;
        color: #fff;
		font-weight: 600;
		font-size: large;
		width: 100%;
        cursor: pointer;
    }

	footer{
		padding: 1rem;
		margin-top: 1rem;
		background-color: #333;
		color: #fff;
		font-size: medium;
		font-weight: 500;
		text-align: center;
	}
</style>