using System.Collections.Generic;
using System.Linq;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class GranuleSetDtoConverter : IGranuleSetDtoConverter
    {
        public GranuleSet ConvertFromDto(GranuleDto[] granulesDto)
        {
            var granuleSet = new GranuleSet();
            var relations = new Dictionary<Granule, int[][]>();

            foreach (var granuleDto in granulesDto)
            {
                var granule = new Granule(granuleDto.Inside, granuleDto.ObjectNumber);

                if (granuleDto.Children != null)
                {
                    relations.Add(granule, granuleDto.Children);
                }

                granuleSet.Add(granule);
            }

            RestoreRelations(relations, granuleSet);
            return granuleSet;
        }

        public GranuleDto[] ConvertToDto(GranuleSet granuleSet)
        {
            var granulesDto = new GranuleDto[granuleSet.Count];

            for (var i = 0; i < granuleSet.Count; i++)
            {
                var granuleDto = new GranuleDto(granuleSet[i].Inside.ToArray(), granuleSet[i].ObjectNumber);

                var children = granuleSet[i].Child;
                if (children.Count > 0)
                {
                    var childrenDto = new int[children.Count][];

                    for (var j = 0; j < children.Count; j++)
                    {
                        childrenDto[j] = children[j].Inside.ToArray();
                    }

                    granuleDto.Children = childrenDto;
                }

                granulesDto[i] = granuleDto;
            }

            return granulesDto;
        }

        private void RestoreRelations(Dictionary<Granule, int[][]> relations, GranuleSet granuleSet)
        {
            foreach (var relation in relations)
            {
                var parent = relation.Key;

                foreach (var item in relation.Value)
                {
                    var child = granuleSet.First(x => x.Inside.SequenceEqual(item));

                    parent.Child.Add(child);
                    child.Parent.Add(parent);
                }
            }
        }
    }
}
