use std::collections::{HashMap, HashSet, VecDeque};

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate},
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day20::solve_part_one(input),
        Part::P2 => Day20::solve_part_two(input),
    }
}

struct Day20;
impl Solution for Day20 {
    fn solve_part_one(input: String) -> String {
        let (start, _, walls) = parse_elements(&input);
        let width = walls.iter().max_by_key(|coor| coor.x).unwrap().x;
        let height = walls.iter().max_by_key(|coor| coor.y).unwrap().y;

        black_sheep_wall(&start, &walls, width, height, 2)
            .values()
            .filter(|&seconds| *seconds >= 100)
            .count()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let (start, _, walls) = parse_elements(&input);
        let width = walls.iter().max_by_key(|coor| coor.x).unwrap().x;
        let height = walls.iter().max_by_key(|coor| coor.y).unwrap().y;

        black_sheep_wall(&start, &walls, width, height, 20)
            .values()
            .filter(|&seconds| *seconds >= 100)
            .count()
            .to_string()
    }
}

fn parse_elements(input: &str) -> (Coordinate, Coordinate, HashSet<Coordinate>) {
    let map = parse_grid(input);
    let start = *map.iter().find(|(_, ch)| **ch == 'S').unwrap().0;
    let end = *map.iter().find(|(_, ch)| **ch == 'E').unwrap().0;
    let walls: HashSet<Coordinate> = map
        .iter()
        .filter_map(|(coor, ch)| match *ch == '#' {
            true => Some(*coor),
            false => None,
        })
        .collect();

    (start, end, walls)
}

fn black_sheep_wall(
    start: &Coordinate,
    walls: &HashSet<Coordinate>,
    width: isize,
    height: isize,
    picoseconds: isize,
) -> HashMap<(Coordinate, Coordinate), u32> {
    let mut queue: VecDeque<_> = [(*start, 0)].into();
    let mut visits: HashMap<Coordinate, u32> = [(*start, 0)].into();

    while let Some((position, cost)) = queue.pop_front() {
        for neighbour in position.neighbours_orthogonal() {
            if &neighbour != start
                && 0 <= neighbour.x
                && neighbour.x < width
                && 0 <= neighbour.y
                && neighbour.y < height
                && !walls.contains(&neighbour)
                && !visits.contains_key(&neighbour)
            {
                queue.push_back((neighbour, cost + 1));
                visits.insert(neighbour, cost + 1);
            }
        }
    }

    let mut cheats: HashMap<(Coordinate, Coordinate), u32> = HashMap::new();
    for (coor, cost) in &visits {
        let within_cheating_distance = visits.iter().filter(|(next_coor, next_cost)| {
            next_cost > &cost && coor.manhattan_distance(&next_coor) <= picoseconds
        });

        for (next_coor, next_cost) in within_cheating_distance {
            let cheat_seconds = coor.manhattan_distance(next_coor);
            if (cost + cheat_seconds as u32) < *next_cost {
                cheats.insert((*coor, *next_coor), next_cost - cost - cheat_seconds as u32);
            }
        }
    }

    cheats
}
