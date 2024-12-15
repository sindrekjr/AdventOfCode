use std::collections::{HashMap, HashSet};

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate},
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day12::solve_part_one(input),
        Part::P2 => Day12::solve_part_two(input),
    }
}

struct Day12;
impl Solution for Day12 {
    fn solve_part_one(input: String) -> String {
        let map = parse_grid(&input);
        let mut visited: HashSet<Coordinate> = HashSet::new();

        map.iter()
            .map(|(coordinate, plant)| {
                let (region, fences) = find_region(coordinate, plant, &map, &mut visited, false);
                region as u32 * fences
            })
            .sum::<u32>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let map = parse_grid(&input);
        let mut visited: HashSet<Coordinate> = HashSet::new();

        map.iter()
            .map(|(coordinate, plant)| {
                let (region, fences) = find_region(coordinate, plant, &map, &mut visited, true);
                region as u32 * fences
            })
            .sum::<u32>()
            .to_string()
    }
}

fn find_region(
    coordinate: &Coordinate,
    plant: &char,
    map: &HashMap<Coordinate, char>,
    visited: &mut HashSet<Coordinate>,
    discount: bool,
) -> (u32, u32) {
    if visited.contains(&coordinate) {
        return (0, 0);
    } else {
        visited.insert(*coordinate);
    }

    let n = coordinate.north();
    let e = coordinate.east();
    let w = coordinate.west();
    let s = coordinate.south();

    let mut mismatches = HashSet::new();

    let (region, fences) = [n, e, w, s]
        .into_iter()
        .fold((1, 0), |(region, fences), neighbour| {
            match map.get(&neighbour) {
                Some(next_plot) if next_plot == plant => {
                    let (sub_region, sub_fences) =
                        find_region(&neighbour, plant, map, visited, discount);

                    (region + sub_region, fences + sub_fences)
                }
                _ if !discount => (region, fences + 1),
                _ => {
                    mismatches.insert(neighbour);

                    if (neighbour == e || neighbour == w) && mismatches.contains(&n) {
                        (region, fences + 1)
                    } else if neighbour == s {
                        match (mismatches.contains(&e), mismatches.contains(&w)) {
                            (true, true) => (region, fences + 2),
                            (true, false) | (false, true) => (region, fences + 1),
                            _ => (region, fences),
                        }
                    } else {
                        (region, fences)
                    }
                }
            }
        });

    if discount {
        let ne = coordinate.north_east();
        let nw = coordinate.north_west();
        let se = coordinate.south_east();
        let sw = coordinate.south_west();

        let corners = [ne, nw, se, sw]
            .into_iter()
            .fold(fences, |corners, diagonal| match map.get(&diagonal) {
                Some(diagonal_plot) if diagonal_plot == plant => corners,
                _ => {
                    if diagonal == ne && !mismatches.contains(&n) && !mismatches.contains(&e) {
                        corners + 1
                    } else if diagonal == nw && !mismatches.contains(&n) && !mismatches.contains(&w)
                    {
                        corners + 1
                    } else if diagonal == se && !mismatches.contains(&s) && !mismatches.contains(&e)
                    {
                        corners + 1
                    } else if diagonal == sw && !mismatches.contains(&s) && !mismatches.contains(&w)
                    {
                        corners + 1
                    } else {
                        corners
                    }
                }
            });

        (region, corners)
    } else {
        (region, fences)
    }
}
