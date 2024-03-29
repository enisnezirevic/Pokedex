import styled from "styled-components";

export const HomeLayout = styled.div`
  display: grid;
  align-items: center;
  max-height: 100vh;
  grid-template-rows: 1fr;
`;

export const HeroSection = styled.section`
  align-items: flex-start;
  display: grid;
  gap: 3.8rem;
  grid-auto-rows: minmax(0, 15rem);
  grid-template-columns: repeat(3, 1fr);
  margin: 3.2rem 4.8rem;

  @media only screen and (max-width: 1245px) {
    grid-template-columns: repeat(2, 1fr);
  }

  @media only screen and (max-width: 957px) {
    grid-template-columns: 1fr;
  }
`;
